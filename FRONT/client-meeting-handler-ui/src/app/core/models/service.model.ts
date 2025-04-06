import {Note} from './note.model';
import {ServiceType} from './service-type.model';

export interface Service {
  Id: string;
  Name: string;
  Date: Date;
  Expiration: Date;
  ServiceTypeId: string;
}

export interface ServiceDetail extends Service {
  ServiceType: ServiceType;
  Notes: Note[];
}
