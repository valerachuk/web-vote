import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { GlobalToastService } from 'src/app/core/services/global-toast.service';
import { Poll } from '../../interfaces/poll.interface';
import { PollService } from '../../services/poll.service';
import { VoteService } from '../../services/vote.service';

@Component({
  selector: 'app-poll-vote-form',
  templateUrl: './poll-vote-form.component.html',
  styleUrls: ['./poll-vote-form.component.css'],
})
export class PollVoteFormComponent implements OnInit {
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
  private pollId: number | null = null;

  public get voteOptionControl(): FormControl {
    return this.form.get('voteOption') as FormControl;
  }

  ngOnInit(): void {
    this.pollId = +this.route.snapshot.params.id;
    this.poll$ = this.pollService.getPollWithOptions(this.pollId);
  }

  public onSubmit(): void {
    this.form.disable();

    this.voteService
      .submitVote({
        pollId: this.pollId as number,
        pollOptionId: this.voteOptionControl.value,
      })
      .subscribe(() => {
        this.toastService.showSuccess('Your vote has been accepted!');
        this.router.navigate(['vote-polls-list'], {
          relativeTo: this.route.parent,
          replaceUrl: true,
        });
      });
  }
}
