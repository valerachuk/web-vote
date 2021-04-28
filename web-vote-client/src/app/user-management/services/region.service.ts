import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { shareReplay } from 'rxjs/operators';
import { Region } from 'src/app/interfaces/region.interface';
import { environment } from 'src/environments/environment';

@Injectable()
export class RegionService {
  constructor(private readonly http: HttpClient) {}

  private regionsCache$: Observable<Array<Region>> | null = null;

  public getRegions(): Observable<Array<Region>> {
    if (!this.regionsCache$) {
      this.regionsCache$ = this.http
        .get<Array<Region>>(`${environment.baseApiUrl}region`)
        .pipe(shareReplay(1));
    }
    return this.regionsCache$;
  }
}
