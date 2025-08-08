import { NgModule } from '@angular/core';
import { ShortenIdPipe } from './pipes/shorten-id.pipe';

@NgModule({
  declarations: [
    ShortenIdPipe
  ],
  exports: [
    ShortenIdPipe
  ]
})
export class SharedModule { }