import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http'

@Injectable({
  providedIn: 'root'
})
export class UploadImageService {

  constructor(private http:HttpClient) { }
  readonly BaseURI = 'https://localhost:44361/api';

  postFile(caption: string, fileToUpload: File) {
    const endpoint = this.BaseURI + '/UploadImage';
    const formData: FormData = new FormData();
    formData.append('Image', fileToUpload);
    formData.append('Name', fileToUpload.name);
    formData.append('ImageCaption', caption);
    console.log(formData);
    console.log(formData.get('Image'))
    console.log(formData.get('Name'))
    console.log(formData.get('ImageCaption'))
    console.log(this.http
      .post(endpoint, formData));
    return this.http
      .post(endpoint, formData, {responseType: 'text'});
  }

}
