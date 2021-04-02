import { Injectable } from '@angular/core';
import { ToastInfo } from '../interfaces/toast-info.interface';

@Injectable()
export class GlobalToastService {
  public toasts: ToastInfo[] = [];

  public show(toast: ToastInfo): void {}

  public showSuccess(message: string): void {
    this.toasts.unshift({
      message,
      delay: 5000,
      styleClass: 'bg-success',
      title: 'Success',
    });
  }

  public showError(message: string): void {
    this.toasts.unshift({
      message,
      delay: -1,
      styleClass: 'bg-danger',
      title: 'Error',
    });
  }

  public remove(toast: ToastInfo): void {
    this.toasts = this.toasts.filter((t) => t !== toast);
  }
}
