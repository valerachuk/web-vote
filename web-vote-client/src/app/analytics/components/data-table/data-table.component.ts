import { Component, Input, OnInit } from '@angular/core';
import { DataTableHeader, DataTableValue } from './data-table-types';

@Component({
  selector: 'app-data-table',
  templateUrl: './data-table.component.html',
  styleUrls: ['./data-table.component.css'],
})
export class DataTableComponent implements OnInit {
  constructor() {}

  @Input()
  public header: DataTableHeader | null = null;

  @Input()
  public value: DataTableValue | null = null;

  public ngOnInit(): void {}
}
