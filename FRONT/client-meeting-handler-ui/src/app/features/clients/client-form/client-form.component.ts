import { CommonModule } from '@angular/common';
import {Component, ElementRef, OnInit, ViewChild} from '@angular/core';
import {FormBuilder, FormGroup, ReactiveFormsModule, Validators} from '@angular/forms';
import {ActivatedRoute, Router, RouterModule} from '@angular/router';
import {ClientService} from '../../../core/services/client.service';
import {Client} from '../../../core/models/client.model';
import {Contact} from '../../../core/models/contact.model';
import {ContactService} from '../../../core/services/contact.service';
import {debounceTime, distinctUntilChanged} from 'rxjs';

@Component({
  selector: 'app-client-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterModule],
  templateUrl: './client-form.component.html'
})
export class ClientFormComponent implements OnInit {
  @ViewChild('contactEmailInput') contactEmailInput!: ElementRef;

  clientForm: FormGroup;
  isEditMode = false;
  clientId: string | null = null;
  loading = false;
  submitted = false;
  error: string | null = null;
  availableContacts: Contact[] = [];
  filteredContacts: Contact[] = [];
  showContactDropdown = false;
  searchTerm = '';
  createNewContactMode = false;

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private clientService: ClientService,
    private contactService: ContactService
  ) {
    this.clientForm = this.fb.group({
      Id: [''],
      Name: ['', Validators.required],
      ContactId: ['', Validators.required],
      contactEmail: [''] // Campo adicional para búsqueda
    });
  }

  get f() { return this.clientForm.controls; }

  ngOnInit(): void {
    this.clientId = this.route.snapshot.paramMap.get('id');
    this.isEditMode = !!this.clientId;

    this.loadAvailableContacts();

    if (this.isEditMode && this.clientId) {
      this.loadClient(this.clientId);
    } else {
      // Para nuevo cliente, generar un nuevo GUID
      this.clientForm.patchValue({
        Id: this.generateGuid()
      });
    }

    // Configurar filtrado de contactos al escribir
    this.clientForm.get('contactEmail')?.valueChanges
      .pipe(
        debounceTime(300),
        distinctUntilChanged()
      )
      .subscribe(value => {
        this.searchTerm = value;
        this.filterContacts();
        // Solo mostrar el dropdown si el usuario está interactuando activamente
        // y no es el llenado inicial de datos
        if (document.activeElement === this.contactEmailInput?.nativeElement) {
          this.showContactDropdown = true;
        }
      });
  }

  loadAvailableContacts(): void {
    this.contactService.getAvailableContacts().subscribe({
      next: (contacts) => {
        this.availableContacts = contacts;
        this.filteredContacts = [...this.availableContacts];

        // Si estamos en modo edición, también incluimos el contacto actual
        if (this.isEditMode && this.clientForm.get('ContactId')?.value) {
          const currentContactId = this.clientForm.get('ContactId')?.value;
          this.contactService.getContactById(currentContactId).subscribe(contact => {
            if (!this.availableContacts.some(c => c.Id === contact.Id)) {
              this.availableContacts.push(contact);
              this.filteredContacts = [...this.availableContacts];
            }
          });
        }
      },
      error: (err) => {
        console.error('Error loading contacts:', err);
      }
    });
  }

  loadClient(id: string): void {
    this.loading = true;
    this.clientService.getClientById(id).subscribe({
      next: (client) => {
        // Guardar estado actual del dropdown
        const currentDropdownState = this.showContactDropdown;

        this.clientForm.patchValue(client);

        // Si el cliente tiene un contacto, cargar su email
        if (client.ContactId) {
          this.contactService.getContactById(client.ContactId).subscribe(contact => {
            // Durante esta asignación, no queremos que se muestre el dropdown
            this.showContactDropdown = false;
            this.clientForm.patchValue({ contactEmail: contact.Email });
            // Reestablecer el estado anterior (aunque probablemente siga siendo falso)
            setTimeout(() => this.showContactDropdown = currentDropdownState, 0);
          });
        }

        this.loading = false;
      },
      error: (err) => {
        console.error('Error loading client:', err);
        this.error = 'Failed to load client data. Please try again later.';
        this.loading = false;
      }
    });
  }

  filterContacts(): void {
    if (!this.searchTerm) {
      this.filteredContacts = [...this.availableContacts];
    } else {
      this.filteredContacts = this.availableContacts.filter(contact =>
        contact.Email.toLowerCase().includes(this.searchTerm.toLowerCase())
      );
    }
  }

  selectContact(contact: Contact): void {
    this.clientForm.patchValue({
      ContactId: contact.Id,
      contactEmail: contact.Email
    });
    this.showContactDropdown = false;
  }

  toggleCreateNewContact(): void {
    this.createNewContactMode = !this.createNewContactMode;
    this.showContactDropdown = false;

    if (!this.createNewContactMode) {
      // Reiniciar la búsqueda cuando cancelamos creación
      this.filterContacts();
    }
  }

  createNewContact(): void {
    // Simple ejemplo - en una app real, usarías un modal o formulario dedicado
    const email = this.clientForm.get('contactEmail')?.value;
    if (!email) {
      this.error = 'Email is required for new contact';
      return;
    }

    const newContact: Contact = {
      Id: this.generateGuid(),
      Email: email,
      Country: '',
      PhoneNumber: 0,
      ClientId: this.clientForm.get('Id')?.value
    };

    this.contactService.createContact(newContact).subscribe({
      next: (contact) => {
        this.clientForm.patchValue({ ContactId: contact.Id });
        this.createNewContactMode = false;
        this.loadAvailableContacts(); // Recargar la lista (aunque este contacto ya estará asociado)
      },
      error: (err) => {
        console.error('Error creating contact:', err);
        this.error = 'Failed to create new contact';
      }
    });
  }

  onSubmit(): void {
    this.submitted = true;

    if (this.clientForm.invalid) {
      return;
    }

    this.loading = true;
    const client: Client = {
      Id: this.clientForm.get('Id')?.value,
      Name: this.clientForm.get('Name')?.value,
      ContactId: this.clientForm.get('ContactId')?.value
    };

    if (this.isEditMode && this.clientId) {
      this.clientService.updateClient(this.clientId, client).subscribe({
        next: () => {
          this.router.navigate(['/clients']);
        },
        error: (err) => {
          console.error('Error updating client:', err);
          this.error = 'Failed to update client. Please try again later.';
          this.loading = false;
        }
      });
    } else {
      this.clientService.createClient(client).subscribe({
        next: () => {
          this.router.navigate(['/clients']);
        },
        error: (err) => {
          console.error('Error creating client:', err);
          this.error = 'Failed to create client. Please try again later.';
          this.loading = false;
        }
      });
    }
  }

  // Helper function to generate a new GUID
  private generateGuid(): string {
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function(c) {
      const r = Math.random() * 16 | 0,
        v = c === 'x' ? r : (r & 0x3 | 0x8);
      return v.toString(16);
    });
  }

  hideDropdown(): void {
    setTimeout(() => {
      this.showContactDropdown = false;
    }, 200);
  }

  protected readonly setTimeout = setTimeout;
}
