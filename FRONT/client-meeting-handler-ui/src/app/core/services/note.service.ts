import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {Note, NoteDetail} from '../models/note.model';
import {environment} from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class NoteService {
  private apiUrl = `${environment.apiUrl}/note`;

  constructor(private http: HttpClient) { }

  getAllNotes(): Observable<Note[]> {
    return this.http.get<Note[]>(`${this.apiUrl}/simple`);
  }

  getNoteById(id: string): Observable<Note> {
    return this.http.get<Note>(`${this.apiUrl}/simple/${id}`);
  }

  getAllNoteDetails(): Observable<NoteDetail[]> {
    return this.http.get<NoteDetail[]>(`${this.apiUrl}/detail`);
  }

  getNoteDetail(id: string): Observable<NoteDetail> {
    return this.http.get<NoteDetail>(`${this.apiUrl}/detail/${id}`);
  }

  createNote(note: Note): Observable<Note> {
    return this.http.post<Note>(this.apiUrl, note);
  }

  updateNote(id: string, note: Note): Observable<Note> {
    return this.http.put<Note>(`${this.apiUrl}/${id}`, note);
  }

  deleteNote(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
