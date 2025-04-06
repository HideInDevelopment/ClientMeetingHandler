import {Note} from './note.model';
import {ServiceType} from './service-type.model';

export interface Service {
  id: string;
  name: string;
  date: Date;
  expiration: Date;
  serviceTypeId: string;
}

export interface ServiceDetail extends Service {
  serviceType: ServiceType;
  notes: Note[];
}
