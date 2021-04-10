import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { VoteForm } from '../../interfaces/vote-form.interface';

@Injectable()
export class VoteService {
  constructor(private readonly http: HttpClient) {}

  public submitVote(voteForm: VoteForm): Observable<void> {
    return this.http.post<void>(`${environment.baseApiUrl}voterVote`, voteForm);
  }
}
