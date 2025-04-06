import {NoteType} from '../enums/note-type.model';
import {Service} from './service.model';

export interface Note {
  id: string;
  title: string;
  content: string;
  noteType: NoteType;
  serviceId: string;
}

export interface NoteDetail extends Note{
  service: Service;
}
