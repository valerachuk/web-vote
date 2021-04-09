import { Injectable } from '@angular/core';
import { ToastInfo } from '../interfaces/toast-info.interface';

@Injectable()
export class GlobalToastService {
  public toasts: ToastInfo[] = [];

  public show(toast: ToastInfo): void {
    this.toasts.unshift(toast);
  }

  public showSuccess(message: string): void {
    this.show({
      message,
      delay: 7000,
      styleClass: 'bg-success',
      title: 'Success',
    });
  }

  public showInfo(message: string): void {
    this.show({
      message,
      delay: 5000,
      title: 'Info',
    });
  }

  public showError(message: string): void {
    this.show({
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
