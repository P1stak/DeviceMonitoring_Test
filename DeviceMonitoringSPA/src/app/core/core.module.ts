import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ShortenIdPipe } from './pipes/shorten-id.pipe';



@NgModule({
  declarations: [
    ShortenIdPipe
  ],
  imports: [
    CommonModule
  ]
})
export class CoreModule { }
