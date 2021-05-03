import { Component, OnInit, TemplateRef } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Observable } from 'rxjs';
import { GlobalToastService } from 'src/app/core/services/global-toast.service';
import { PollInfo } from '../../../interfaces/poll-info.interface';
import { PollService } from '../../../core/services/poll.service';
import { PollsViewType } from '../../../constants/polls-view-type.enum';
import { DEFAULT_DATE_TIME_FORMAT } from 'src/app/constants/misc.constant';

@Component({
  selector: 'app-view-edit-vote-polls-list',
  templateUrl: './view-edit-vote-polls-list.component.html',
  styleUrls: ['./view-edit-vote-polls-list.component.css'],
})
export class ViewEditVotePollsListComponent implements OnInit {
  constructor(
    private readonly pollSerivce: PollService,
    private readonly modalService: NgbModal,
    private readonly toastService: GlobalToastService,
    private readonly route: ActivatedRoute
  ) {}

  public pollsInfo$: Observable<Array<PollInfo>> | null = null;
  public pollToDelete: PollInfo | null = null;
  public pollsViewType: PollsViewType | null = null;
  public readonly PollsViewType = PollsViewType;
  public readonly defaultDateTimeFormat = DEFAULT_DATE_TIME_FORMAT;

  public ngOnInit(): void {
    this.pollsViewType = this.route.snapshot.data
      .pollsViewType as PollsViewType;

    switch (this.pollsViewType) {
      case PollsViewType.Pending:
        this.pollsInfo$ = this.pollSerivce.getPendingPolls();
        break;
      case PollsViewType.Active:
        this.pollsInfo$ = this.pollSerivce.getActivePolls();
        break;
      case PollsViewType.Archive:
        this.pollsInfo$ = this.pollSerivce.getArchivedPolls();
        break;
    }
  }

  public openDeletePollModal(
    pollInfo: PollInfo,
    modal: TemplateRef<any>
  ): void {
    this.pollToDelete = pollInfo;
    this.modalService
      .open(modal)
      .result.then(() => {
        this.pollSerivce.deletePoll(pollInfo.id).subscribe(() => {
          this.toastService.showSuccess(
            `Poll "${pollInfo.title}" successfuly deleted`
          );
          this.ngOnInit();
        });
      })
      .catch(() => {});
  }
}
