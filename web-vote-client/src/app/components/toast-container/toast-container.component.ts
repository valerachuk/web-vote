import { Component } from '@angular/core';
import { GlobalToastService } from 'src/app/core/services/global-toast.service';

@Component({
  selector: 'app-toast-container',
  templateUrl: './toast-container.component.html',
  styleUrls: ['./toast-container.component.css'],
})
export class ToastContainerComponent {
  constructor(public readonly toastService: GlobalToastService) {}
}
