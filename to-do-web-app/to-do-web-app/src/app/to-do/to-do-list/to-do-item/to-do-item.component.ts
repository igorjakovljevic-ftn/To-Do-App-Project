import { ToDoIdService } from './../../../core/to-do-id.service';
import { Router } from '@angular/router';
import { ToDoListDataService } from 'src/app/core/to-do-list-data.service';
import { ListItem } from '../../../models/list-item';
import { Component, Input, OnDestroy, OnInit, Output } from '@angular/core';
import { EventEmitter } from '@angular/core';
import { Subscription } from 'rxjs';


@Component({
  selector: 'app-to-do-item',
  templateUrl: './to-do-item.component.html',
  styleUrls: ['./to-do-item.component.css']
})
export class ToDoItemComponent implements OnInit, OnDestroy{
  @Input('listItem') listItem = new ListItem();
  @Input('listId') listId: string | undefined;
  @Output() itemCreated = new EventEmitter();
  @Output() itemDeleted = new EventEmitter();
  subscription!: Subscription;

  constructor(private service: ToDoListDataService, private router: Router, private idService: ToDoIdService) { 
    this.subscription = this.idService.getObservable().subscribe(
      id => {
        this.listId = id;
        this.createItem();
      }
    );
  }
  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  ngOnInit(): void {
  }

  public updateListItem(item: ListItem){
    if(this.listItem.id != undefined){
      this.service.updateItem(item).subscribe();
    }else{
      if(this.listId != undefined){
        this.createItem();
      }else{
        this.idService.createToDoList();
      }
    }
  }

  private createItem(){
    this.service.addListItem(this.listId, this.listItem).subscribe(
      (response) => {
        this.itemCreated.emit(response);
        this.listItem = new ListItem();
      }
    );    
  }

  public deleteListItem(){
    this.service.deleteListItem(this.listId, this.listItem.id).subscribe(
      (response) => this.itemDeleted.emit(response)
    );
  }
}
