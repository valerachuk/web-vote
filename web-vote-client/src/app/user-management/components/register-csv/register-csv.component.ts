import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { finalize } from 'rxjs/operators';
import { CSV_USERS_MAX_FILE_SIZE } from 'src/app/constants/misc.constant';
import { GlobalToastService } from 'src/app/core/services/global-toast.service';
import { UserManagementService } from '../../services/user-management.service';

@Component({
  selector: 'app-register-csv',
  templateUrl: './register-csv.component.html',
  styleUrls: ['./register-csv.component.css'],
})
export class RegisterCsvComponent implements OnInit {
  constructor(
    private readonly userManagementService: UserManagementService,
    private readonly globalToastService: GlobalToastService
  ) {}

  @ViewChild('fileInput')
  private fileInput: ElementRef<HTMLInputElement> | null = null;

  private defaultFileNamePlaceholder = 'Choose file';
  public fileNamePlaceholder: string | null = null;
  public serverSideError = '';

  public csvUsersMaxFileSize = CSV_USERS_MAX_FILE_SIZE;
  public fileNotChosenError = false;
  public fileTooLargeError = false;
  public isFileUploading = false;

  public get chosenFile(): File | undefined {
    return this.fileInput!.nativeElement.files![0];
  }

  public ngOnInit(): void {
    this.fileNamePlaceholder = this.defaultFileNamePlaceholder;
  }

  public onFileChosen(): void {
    const pendingFileName = this.chosenFile?.name;

    this.fileNamePlaceholder =
      pendingFileName || this.defaultFileNamePlaceholder;

    this.serverSideError = '';
    this.validateInput();
  }

  public onSubmit(): void {
    this.validateInput();

    if (this.fileNotChosenError || this.fileTooLargeError) {
      return;
    }

    this.isFileUploading = true;

    this.userManagementService
      .registerVotersCsv(this.chosenFile!)
      .pipe(
        finalize(() => {
          this.isFileUploading = false;
        })
      )
      .subscribe(
        () => {
          const { nativeElement } = this.fileInput!;
          nativeElement.value = '';
          nativeElement.dispatchEvent(new Event('change'));
          this.fileNotChosenError = false;

          this.globalToastService.showSuccess('Successfuly registered users');
        },
        (error) => {
          if (error.status === 400 || error.status === 409) {
            this.serverSideError = error.error;
            return;
          }
          throw error;
        }
      );
  }

  private validateInput(): void {
    this.fileNotChosenError = this.chosenFile === undefined;
    this.fileTooLargeError =
      this.chosenFile !== undefined &&
      this.chosenFile.size > this.csvUsersMaxFileSize;
  }
}
