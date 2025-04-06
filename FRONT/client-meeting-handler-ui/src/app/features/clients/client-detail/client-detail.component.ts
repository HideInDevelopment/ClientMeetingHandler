import {Component, OnInit} from "@angular/core";
import {CommonModule} from '@angular/common';
import {ActivatedRoute, RouterModule} from '@angular/router';
import {ClientDetail} from '../../../core/models/client.model';
import {ClientService} from '../../../core/services/client.service';

@Component({
  selector: 'app-client-detail',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: 'client-detail.component.html'
})

export class ClientDetailComponent implements OnInit {
  client: ClientDetail | null = null;
  loading = false;
  error: string | null = null;

  constructor(
    private route: ActivatedRoute,
    private clientService: ClientService
  ) { }

  ngOnInit(): void {
    this.loading = true;
    const id = this.route.snapshot.paramMap.get('id');

    if (id) {
      this.clientService.getClientDetail(id).subscribe({
        next: (data) => {
          this.client = data;
          this.loading = false;
        },
        error: (err) => {
          console.error('Error loading client details:', err);
          this.error = 'Failed to load client details. Please try again later.';
          this.loading = false;
        }
      });
    } else {
      this.error = 'No client ID provided.';
      this.loading = false;
    }
  }
}
