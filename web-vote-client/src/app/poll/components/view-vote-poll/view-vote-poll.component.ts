import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { GlobalToastService } from 'src/app/core/services/global-toast.service';
import { Poll } from '../../../interfaces/poll.interface';
import { PollService } from '../../../core/services/poll.service';
import { VoteService } from '../../services/vote.service';

@Component({
  selector: 'app-view-vote-poll',
  templateUrl: './view-vote-poll.component.html',
  styleUrls: ['./view-vote-poll.component.css'],
})
export class ViewVotePollComponent implements OnInit {
  constructor(
    private readonly pollService: PollService,
    private readonly voteService: VoteService,
    private readonly toastService: GlobalToastService,
    private readonly route: ActivatedRoute,
    private readonly router: Router
  ) {}

  public poll$: Observable<Poll> | null = null;

  public readonly form = new FormGroup({
    voteOption: new FormControl(null, Validators.required),
  });

  public isViewForm = false;

  public get voteOptionControl(): FormControl {
    return this.form.get('voteOption') as FormControl;
  }

  ngOnInit(): void {
    this.isViewForm = this.route.snapshot.data.isViewForm || false;
    const pollId = +this.route.snapshot.params.id;
    this.poll$ = this.pollService.getPollWithOptions(pollId);
  }

  public onSubmit(): void {
    this.form.disable();

    this.voteService
      .submitVote({
        pollOptionId: this.voteOptionControl.value,
      })
      .subscribe(() => {
        this.toastService.showSuccess('Your vote has been accepted!');
        this.router.navigate(['active-polls-list'], {
          relativeTo: this.route.parent,
          replaceUrl: true,
        });
      });
  }
}
