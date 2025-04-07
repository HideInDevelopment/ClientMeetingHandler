import {Client} from './client.model';

export interface Contact {
  Id: String;
  Country: string;
  PhoneNumber: number;
  Email: string;
  ClientId: string;
}

export interface ContactDetail extends Contact {
  Client: Client
}
