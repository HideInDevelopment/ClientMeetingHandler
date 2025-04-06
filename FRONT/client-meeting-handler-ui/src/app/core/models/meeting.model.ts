import {Client} from './client.model';
import {Location} from './location.model';

export interface Meeting {
  Id: string;
  Date: Date;
  Duration: number;
  LocationId: string;
  ClientId: string;
}

export interface MeetingDetail extends Meeting {
  Location: Location;
  Client: Client;
}
