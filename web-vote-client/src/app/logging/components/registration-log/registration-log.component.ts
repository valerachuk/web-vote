import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { RegistrationLogRecord } from 'src/app/interfaces/registration-log-record.interface';
import { DataTableHeader } from 'src/app/shared/components/data-table/data-table-types';
import { LoggingService } from '../../services/logging.service';

@Component({
  selector: 'app-registration-log',
  templateUrl: './registration-log.component.html',
  styleUrls: ['./registration-log.component.css'],
})
export class RegistrationLogComponent implements OnInit {
  constructor(private readonly loggingService: LoggingService) {}

  public registrationLog$: Observable<
    Array<RegistrationLogRecord>
  > | null = null;

  public readonly tableHeader: DataTableHeader = [
    {
      columnName: 'Timestamp UTC',
      key: 'timestamp',
    },
    {
      columnName: 'To whom name',
      key: 'toWhomName',
    },
    {
      columnName: 'To whom ITN',
      key: 'toWhomITN',
    },
    {
      columnName: 'By whom name',
      key: 'byWhomName',
    },
    {
      columnName: 'By whom ITN',
      key: 'byWhomITN',
    },
  ];

  ngOnInit(): void {
    this.registrationLog$ = this.loggingService.getRegistrationLog();
  }
}
