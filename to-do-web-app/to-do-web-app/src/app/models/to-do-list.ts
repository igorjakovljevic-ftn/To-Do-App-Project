import { ListItem } from "./list-item";


export class ToDoList{
    id: string | undefined;
    title: string | undefined;
    position: number | undefined;
    listItems = new Array<ListItem>();
}
