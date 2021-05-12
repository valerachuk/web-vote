import { DatePipe } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { DEFAULT_DATE_TIME_FORMAT } from 'src/app/constants/misc.constant';
import { RegistrationLogRecord } from 'src/app/interfaces/registration-log-record.interface';
import { environment } from 'src/environments/environment';

@Injectable()
export class LoggingService {
  constructor(
    private readonly http: HttpClient,
    private readonly datePipe: DatePipe
  ) {}

  public getRegistrationLog(): Observable<Array<RegistrationLogRecord>> {
    return this.http
      .get<Array<RegistrationLogRecord>>(
        `${environment.baseApiUrl}logging/registration-log`
      )
      .pipe(
        map((regisrationLog) =>
          regisrationLog.map(
            (regisrationLogRecord) =>
              ({
                ...regisrationLogRecord,
                timestamp: this.datePipe.transform(
                  regisrationLogRecord.timestamp,
                  DEFAULT_DATE_TIME_FORMAT,
                  'utc'
                ),
              } as RegistrationLogRecord)
          )
        )
      );
  }
}
