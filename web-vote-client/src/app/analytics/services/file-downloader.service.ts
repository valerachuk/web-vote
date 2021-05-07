import { HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { saveAs } from 'file-saver';

@Injectable()
export class FileDownloaderService {
  constructor() {}

  public saveFile(httpResponse: HttpResponse<Blob>): void {
    const uriEncodedFileName = httpResponse.headers
      .get('content-disposition')!
      .match(/filename\*=UTF-8''(.*?)$/)![1];

    const fileName = decodeURI(uriEncodedFileName);
    const file = new File([httpResponse.body!], fileName, {
      type: httpResponse.body!.type,
    });
    saveAs(file);
  }
}
