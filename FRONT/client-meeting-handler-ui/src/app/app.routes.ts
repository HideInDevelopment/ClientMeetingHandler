import { Routes } from '@angular/router';
import {ClientListComponent} from './features/clients/client-list/client-list.component';
import {ClientDetailComponent} from './features/clients/client-detail/client-detail.component';
import {ClientFormComponent} from './features/clients/client-form/client-form.component';

export const routes: Routes = [
  { path: '', redirectTo: '/clients', pathMatch: 'full' },
  { path: 'clients', component: ClientListComponent },
  { path: 'clients/new', component: ClientFormComponent },
  { path: 'clients/:id', component: ClientDetailComponent },
  { path: 'clients/:id/edit', component: ClientFormComponent },
  { path: '**', redirectTo: '/clients' }
];
