import {Component, OnInit} from '@angular/core';
import {ClientService} from '../../../core/services/client.service';
import {Client} from '../../../core/models/client.model';
import {CommonModule} from '@angular/common';
import {RouterModule} from '@angular/router';

@Component({
  selector: 'app-client-list',
  standalone: true,
  imports: [CommonModule, RouterModule],
  template: './client-list.component.html'
})
export class ClientListComponent implements OnInit {

  clients: Client[] = [];
  loading = false;
  error: string | null = null;

  constructor(private clientService: ClientService) { }
  ngOnInit(): void {
    this.loadClients();
  }

  loadClients(): void {
    this.loading = true;
    this.error = null;

    this.clientService.getAllClients().subscribe({
      next: (data) => {
        this.clients = data;
        this.loading = false;
      },
      error: (err) => {
        console.error('Error loading clients:', err);
        this.error = 'Failed to load clients. Please try again later.';
        this.loading = false;
      }
    });
  }

  deleteClient(id: string): void {
    if (confirm('Are you sure you want to delete this client?')) {
      this.clientService.deleteClient(id).subscribe({
        next: () => {
          this.clients = this.clients.filter(client => client.id !== id);
        },
        error: (err) => {
          console.error('Error deleting client:', err);
          this.error = 'Failed to delete client. Please try again later.';
        }
      });
    }
  }
}

