import { ToDoListComponent } from './../../../to-do/to-do-list/to-do-list.component';
import { ToDoListDataService } from './../../../core/to-do-list-data.service';
import { Component, Input, Output, ViewChild } from '@angular/core';
import { ToDoList } from 'src/app/models/to-do-list';
import { Router } from '@angular/router';
import { EventEmitter } from '@angular/core';

@Component({
  selector: 'app-to-do-preview',
  templateUrl: './to-do-preview.component.html',
  styleUrls: ['./to-do-preview.component.css']
})
export class ToDoPreviewComponent{
  @Input('selected_list') list: ToDoList = new ToDoList();
  @ViewChild(ToDoListComponent) toDoList!: ToDoListComponent;
  @Output() deletedList = new EventEmitter();

  constructor(private service: ToDoListDataService, private router: Router) {}

  get completed(){
    return this.list.listItems.filter(x => x.isCompleted);
  }

  get notCompleted(){
    return this.list.listItems.filter(x => !x.isCompleted);
  }

  public delete(id: string | undefined){
    this.service.deleteList(id).subscribe();
    this.deletedList.emit("refresh");
  }
}
