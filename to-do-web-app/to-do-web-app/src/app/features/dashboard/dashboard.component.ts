import { ToDoList } from './../../models/to-do-list';
import { Component, OnInit } from '@angular/core';
import { ToDoListDataService } from 'src/app/core/to-do-list-data.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  lists: Array<ToDoList> = new Array<ToDoList>();
  
  constructor(private service: ToDoListDataService) { }

  ngOnInit(): void {
      this.getLists();
  }

  private getLists(){
      this.service.getLists().subscribe(
        data => {
          this.lists = data;
        }
      );
  }

  onDeleted($event:any){
    this.getLists();
  }

}
