import { Component, OnInit, TemplateRef } from '@angular/core';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { Observable } from 'rxjs';
import { PollInfo } from '../../interfaces/poll-info.interface';
import { PollService } from '../../services/poll.service';

@Component({
  selector: 'app-view-edit-vote-polls-list',
  templateUrl: './view-edit-vote-polls-list.component.html',
  styleUrls: ['./view-edit-vote-polls-list.component.css'],
})
export class ViewEditVotePollsListComponent implements OnInit {
  constructor(
    private readonly pollSerivce: PollService,
    private readonly modalService: NgbModal
  ) {}

  public pollsInfo$: Observable<Array<PollInfo>> | null = null;
  public pollToDelete: PollInfo | null = null;

  public ngOnInit(): void {
    this.pollsInfo$ = this.pollSerivce.getPollsInfo();
  }

  public openDeletePollModal(
    pollInfo: PollInfo,
    modal: TemplateRef<any>
  ): void {
    this.pollToDelete = pollInfo;
    this.modalService
      .open(modal)
      .result.catch(() => {})
      .then(() => {
        this.pollSerivce.deletePoll(pollInfo.id).subscribe(() => {
          this.ngOnInit();
        });
      });
  }
}
