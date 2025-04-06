import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {Meeting, MeetingDetail} from '../models/meeting.model';
import {environment} from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class MeetingService {
  private apiUrl = `${environment.apiUrl}/meeting`;

  constructor(private http: HttpClient) { }

  getAllMeetings(): Observable<Meeting[]> {
    return this.http.get<Meeting[]>(`${this.apiUrl}/simple`);
  }

  getMeetingById(id: string): Observable<Meeting> {
    return this.http.get<Meeting>(`${this.apiUrl}/simple/${id}`);
  }

  getAllMeetingDetails(): Observable<MeetingDetail[]> {
    return this.http.get<MeetingDetail[]>(`${this.apiUrl}/detail`);
  }

  getMeetingDetail(id: string): Observable<MeetingDetail> {
    return this.http.get<MeetingDetail>(`${this.apiUrl}/detail/${id}`);
  }

  createMeeting(meeting: Meeting): Observable<Meeting> {
    return this.http.post<Meeting>(this.apiUrl, meeting);
  }

  updateMeeting(id: string, meeting: Meeting): Observable<Meeting> {
    return this.http.put<Meeting>(`${this.apiUrl}/${id}`, meeting);
  }

  deleteMeeting(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
