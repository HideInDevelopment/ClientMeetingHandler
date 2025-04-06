import {Meeting} from './meeting.model';

export interface Location {
  id: string;
  country: string;
  city: string;
  street: string;
}

export interface LocationDetail extends Location {
  meetings: Meeting[];
}
