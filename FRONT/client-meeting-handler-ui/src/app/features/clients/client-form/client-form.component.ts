import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { ClientService } from '../../../core/services/client.service';
import { Client } from '../../../core/models/client.model';
import { Contact } from '../../../core/models/contact.model';
import { ContactService } from '../../../core/services/contact.service';
import { ContactModalComponent } from '../../contacts/contact-modal/contact-modal.component';

@Component({
  selector: 'app-client-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterModule, ContactModalComponent],
  templateUrl: './client-form.component.html'
})
export class ClientFormComponent implements OnInit {
  clientForm: FormGroup;
  isEditMode = false;
  clientId: string | null = null;
  loading = false;
  submitted = false;
  error: string | null = null;

  // Contact related properties
  hasContact = false;
  contactEmail = '';
  contactDetails: Contact | null = null;
  showContactModal = false;

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
      ContactId: ['']
    });
  }

  get f() { return this.clientForm.controls; }

  ngOnInit(): void {
    this.clientId = this.route.snapshot.paramMap.get('id');
    this.isEditMode = !!this.clientId;

    if (this.isEditMode && this.clientId) {
      this.loadClient(this.clientId);
    } else {
      // For new client, generate a new GUID
      this.clientForm.patchValue({
        Id: this.generateGuid()
      });
    }
  }

  loadClient(id: string): void {
    this.loading = true;
    this.clientService.getClientDetail(id).subscribe({
      next: (client) => {
        this.clientForm.patchValue({
          Id: client.Id,
          Name: client.Name,
          ContactId: client.ContactId
        });

        // Check if client has a contact
        if (client.Contact && client.ContactId) {
          console.log("Cliente tiene contacto:", client.Contact);
          this.hasContact = true;
          this.contactEmail = client.Contact.Email;
          this.contactDetails = client.Contact;
          this.clientForm.patchValue({ ContactId: client.ContactId });
        } else {
          console.log("Cliente no tiene contacto");
          this.hasContact = false;
          this.contactDetails = null;
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

  openContactModal(): void {
    // Asegurarnos de que isEditMode esté correctamente configurado
    console.log("Opening modal with hasContact:", this.hasContact);
    this.showContactModal = true;
  }

  closeContactModal(): void {
    this.showContactModal = false;
  }

  saveContact(contact: Contact): void {
    if (this.hasContact) {
      // Update existing contact
      this.contactService.updateContact(contact.Id, contact).subscribe({
        next: (updatedContact) => {
          this.contactDetails = updatedContact;
          this.contactEmail = updatedContact.Email;
          this.clientForm.patchValue({ ContactId: updatedContact.Id });
          this.closeContactModal();
        },
        error: (err) => {
          console.error('Error updating contact:', err);
          this.error = 'Failed to update contact. Please try again later.';
        }
      });
    } else {
      // Create new contact
      // Assign client ID to contact
      contact.ClientId = this.clientForm.get('Id')?.value;

      this.contactService.createContact(contact).subscribe({
        next: (createdContact) => {
          this.hasContact = true;
          this.contactDetails = createdContact;
          this.contactEmail = createdContact.Email;
          this.clientForm.patchValue({ ContactId: createdContact.Id });
          this.closeContactModal();
        },
        error: (err) => {
          console.error('Error creating contact:', err);
          this.error = 'Failed to create contact. Please try again later.';
        }
      });
    }
  }

  onSubmit(): void {
    this.submitted = true;

    // Check if form is valid
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
}
