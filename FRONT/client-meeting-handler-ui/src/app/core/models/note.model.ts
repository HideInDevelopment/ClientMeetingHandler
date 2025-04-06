import {NoteType} from '../enums/note-type.model';
import {Service} from './service.model';

export interface Note {
  Id: string;
  Title: string;
  Content: string;
  NoteType: NoteType;
  ServiceId: string;
}

export interface NoteDetail extends Note{
  Service: Service;
}
