import { ToDoListDataService } from 'src/app/core/to-do-list-data.service';
import { Injectable } from '@angular/core';
import { Subject, Observable } from 'rxjs';
import { ToDoList } from '../models/to-do-list';

@Injectable({
  providedIn: 'root'
})
export class ToDoIdService {
  private subject = new Subject<any>();
  
  constructor(private service: ToDoListDataService) {}

  getObservable(): Observable<any>{
    return this.subject.asObservable();
  }

  createToDoList(){
    this.service.addList(new ToDoList()).subscribe(
      response => this.subject.next(response.id)
    );
  }
}
