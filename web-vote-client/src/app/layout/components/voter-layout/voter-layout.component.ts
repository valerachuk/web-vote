import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-voter-layout',
  templateUrl: './voter-layout.component.html',
  styleUrls: ['./voter-layout.component.css'],
})
export class VoterLayoutComponent implements OnInit {
  constructor() {}

  public isMenuCollapsed = true;

  ngOnInit(): void {}
}
