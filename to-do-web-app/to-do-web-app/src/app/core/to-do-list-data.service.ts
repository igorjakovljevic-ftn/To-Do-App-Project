import { ListItem } from './../models/list-item';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ToDoList } from '../models/to-do-list';
import { environment } from 'src/environments/environment';


@Injectable({
  providedIn: 'root'
})
export class ToDoListDataService {
  list!: ToDoList;

  constructor(private httpClient: HttpClient) { }


  getLists(): Observable<Array<ToDoList>>{
    return this.httpClient.get<Array<ToDoList>>(`${environment.baseUrl}/to-do-lists`);
  }

  getListItems(id: string | undefined): Observable<Array<ListItem>> {
    return this.httpClient.get<Array<ListItem>>(`${environment.baseUrl}/to-do-lists/${id}/list-items`);
  }

  getList(id: string | undefined): Observable<ToDoList> {
    return this.httpClient.get<ToDoList>(`${environment.baseUrl}/to-do-lists/${id}`);
  }

  updateItem(item: ListItem): Observable<ListItem>{
    return this.httpClient.put<ListItem>(`${environment.baseUrl}/to-do-lists/${item.toDoListId}/list-items/${item.id}`, item);
  }

  updateList(list: ToDoList): Observable<ToDoList>{
    return this.httpClient.put<ToDoList>(`${environment.baseUrl}/to-do-lists/${list.id}`, list);
  }

  addListItem(listId: string | undefined, item: ListItem): Observable<ListItem>{
    return this.httpClient.post<ListItem>(`${environment.baseUrl}/to-do-lists/${listId}/list-items`, item);
  }

  addList(list: ToDoList): Observable<ToDoList>{
    return this.httpClient.post<ToDoList>(`${environment.baseUrl}/to-do-lists`, list);
  }

  deleteList(id: string | undefined): Observable<void>{
    return this.httpClient.delete<void>(`${environment.baseUrl}/to-do-lists/${id}`);
  }

  deleteListItem(listId: string | undefined, listItemId: string | undefined): Observable<ListItem>{
    return this.httpClient.delete<ListItem>(`${environment.baseUrl}/to-do-lists/${listId}/list-items/${listItemId}`);
  }
}
