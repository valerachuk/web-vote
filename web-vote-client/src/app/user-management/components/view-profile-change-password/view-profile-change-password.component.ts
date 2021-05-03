import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { DEFAULT_DATE_FORMAT } from 'src/app/constants/misc.constant';
import { PersonInfo } from 'src/app/interfaces/person-info.interface';
import { UserManagementService } from '../../services/user-management.service';

@Component({
  selector: 'app-view-profile-change-password',
  templateUrl: './view-profile-change-password.component.html',
  styleUrls: ['./view-profile-change-password.component.css'],
})
export class ViewProfileChangePasswordComponent implements OnInit {
  constructor(private readonly userManagementService: UserManagementService) {}

  public personInfo$: Observable<PersonInfo> | null = null;

  public readonly defaultDateFormat = DEFAULT_DATE_FORMAT;

  ngOnInit(): void {
    this.personInfo$ = this.userManagementService.getProfileInfo();
  }
}
