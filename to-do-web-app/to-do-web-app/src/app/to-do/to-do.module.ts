import { RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ToDoListComponent } from './to-do-list/to-do-list.component';
import { FormsModule } from '@angular/forms';
import { ToDoItemComponent } from './to-do-list/to-do-item/to-do-item.component';




@NgModule({
  declarations: [
    ToDoListComponent,
    ToDoItemComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    RouterModule
  ],
  exports: [
    ToDoListComponent
  ]
})
export class ToDoCreateEditModule { }
