import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { PollInfo } from '../../interfaces/poll-info.interface';
import { PollService } from '../../services/poll.service';

@Component({
  selector: 'app-view-edit-vote-polls-list',
  templateUrl: './view-edit-vote-polls-list.component.html',
  styleUrls: ['./view-edit-vote-polls-list.component.css'],
})
export class ViewEditVotePollsListComponent implements OnInit {
  constructor(private readonly pollSerivce: PollService) {}

  public pollsInfo$: Observable<Array<PollInfo>> | null = null;

  ngOnInit(): void {
    this.pollsInfo$ = this.pollSerivce.getPollsInfo();
  }
}
