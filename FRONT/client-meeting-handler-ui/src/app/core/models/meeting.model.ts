import {Client} from './client.model';
import {Location} from './location.model';

export interface Meeting {
  id: string;
  date: Date;
  duration: number;
  locationId: string;
  clientId: string;
}

export interface MeetingDetail extends Meeting {
  location: Location;
  client: Client;
}
