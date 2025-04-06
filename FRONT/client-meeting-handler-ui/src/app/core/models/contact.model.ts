import {Client} from './client.model';

export interface Contact {
  country: string;
  phoneNumber: number;
  email: string;
  clientId: string;
}

export interface ContactDetail extends Contact {
  client: Client
}
