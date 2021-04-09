import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse,
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { AuthService } from './auth.service';
import { GlobalToastService } from './global-toast.service';

@Injectable()
export class HTTPErrorInterceptor implements HttpInterceptor {
  constructor(
    private readonly authService: AuthService,
    private readonly toastService: GlobalToastService
  ) {}

  private errorHandler(err: any, ngiSkip: string | null): void {
    if (!(err instanceof HttpErrorResponse)) {
      this.toastService.showError('Unknown error');
      return;
    }

    const httpErr = err as HttpErrorResponse;

    if (ngiSkip != null && httpErr.status === +ngiSkip) {
      return;
    }

    if (httpErr.status === 401) {
      this.authService.signOut();
      this.toastService.showInfo('Login again, please');
      return;
    }

    this.toastService.showError(
      typeof httpErr.error === 'object'
        ? JSON.stringify(httpErr.error)
        : httpErr.error
    );
  }

  public intercept(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    const NGI_SKIP = 'ngiSkip';
    let ngiSkip: string | null = null;

    const { headers: requestHeaders } = request;
    if (requestHeaders.has(NGI_SKIP)) {
      ngiSkip = requestHeaders.get(NGI_SKIP);
      const headers = requestHeaders.delete(NGI_SKIP);
      request = request.clone({ headers });
    }

    return next.handle(request).pipe(
      tap(
        () => {},
        (err) => this.errorHandler(err, ngiSkip)
      )
    );
  }
}
