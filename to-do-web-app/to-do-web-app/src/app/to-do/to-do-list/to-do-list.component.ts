import { Subscription } from 'rxjs';
import { ToDoIdService } from './../../core/to-do-id.service';
import { ListItem } from './../../models/list-item';
import { ToDoList } from './../../models/to-do-list';
import { ToDoListDataService } from './../../core/to-do-list-data.service';
import { Component, Input, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-to-do-list',
  templateUrl: './to-do-list.component.html',
  styleUrls: ['./to-do-list.component.css']
})
export class ToDoListComponent implements OnInit, OnDestroy {
  id!: string;
  private sub: any;
  list: ToDoList = new ToDoList();
  item: ListItem = new ListItem();
  newList: ToDoList = new ToDoList();
  isAddMode!: boolean;
  subcription!: Subscription;

  constructor(private service: ToDoListDataService, private route: ActivatedRoute, private router: Router, private idService: ToDoIdService) {
    this.subcription = this.idService.getObservable().subscribe(
      id => this.list.id = id
    );
  }

  get completed() {
    return this.list.listItems.filter(x => x.isCompleted);
  }

  get notCompleted() {
    return this.list.listItems.filter(x => !x.isCompleted);
  }

  ngOnDestroy(): void {
    this.subcription.unsubscribe();
  }

  ngOnInit(): void {
    this.sub = this.route.params.subscribe(
      params => {
        this.id = params['id'];
      }
    );
    if (this.id == undefined) {
      this.isAddMode = true;
    } else if (this.id != undefined) {
      this.isAddMode = false;
    }
    this.getList(this.id);
  }

  private getList(id: string | undefined) {
    this.service.getList(id).subscribe(
      data => {
        this.list = data;
      }
    );
  }

  public updateTitle() {
    if (this.list.id != undefined) {
      this.service.updateList(this.list).subscribe();
    }
    else {
      this.addList();
    }
  }

  public triggerAddList($event: any) {
    this.list.title = "";
    this.addList();
  }

  public addList() {
    this.service.addList(this.list).subscribe(
      data => {
        this.list = data;
      }
    );
  }

  onCreated(item: ListItem) {
    this.list.listItems.push(item);
  }

  onDeleted(){
    this.getList(this.list.id);
  }
}
