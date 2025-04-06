import { CommonModule } from '@angular/common';
import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, ReactiveFormsModule, Validators} from '@angular/forms';
import {ActivatedRoute, Router, RouterModule} from '@angular/router';
import {ClientService} from '../../../core/services/client.service';
import {Client} from '../../../core/models/client.model';

@Component({
  selector: 'app-client-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterModule],
  templateUrl: 'client-form.component.html'
})
export class ClientFormComponent implements OnInit {
  clientForm: FormGroup;
  isEditMode = false;
  clientId: string | null = null;
  loading = false;
  submitted = false;
  error: string | null = null;

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private clientService: ClientService
  ) {
    this.clientForm = this.fb.group({
      id: [''],
      name: ['', Validators.required],
      contactId: ['', Validators.required]
    });
  }

  get f() { return this.clientForm.controls; }

  ngOnInit(): void {
    this.clientId = this.route.snapshot.paramMap.get('id');
    this.isEditMode = !!this.clientId;

    if (this.isEditMode && this.clientId) {
      this.loadClient(this.clientId);
    } else {
      // For new clients, generate a new GUID
      this.clientForm.patchValue({
        id: this.generateGuid()
      });
    }
  }

  loadClient(id: string): void {
    this.loading = true;
    this.clientService.getClientById(id).subscribe({
      next: (client) => {
        this.clientForm.patchValue(client);
        this.loading = false;
      },
      error: (err) => {
        console.error('Error loading client:', err);
        this.error = 'Failed to load client data. Please try again later.';
        this.loading = false;
      }
    });
  }

  onSubmit(): void {
    this.submitted = true;

    if (this.clientForm.invalid) {
      return;
    }

    this.loading = true;
    const client: Client = this.clientForm.value;

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
