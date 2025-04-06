import {Meeting} from './meeting.model';

export interface Location {
  Id: string;
  Country: string;
  City: string;
  Street: string;
}

export interface LocationDetail extends Location {
  Meetings: Meeting[];
}
