import { Component, OnInit, TemplateRef } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Observable } from 'rxjs';
import { GlobalToastService } from 'src/app/core/services/global-toast.service';
import { PollInfo } from '../../interfaces/poll-info.interface';
import { PollService } from '../../services/poll.service';
import { PollsViewType } from '../../../constants/polls-view-type.enum';

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

  public ngOnInit(): void {
    this.pollsViewType = this.route.snapshot.data
      .pollsViewType as PollsViewType;
    this.pollsInfo$ = this.pollSerivce.getPollsInfo();
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
