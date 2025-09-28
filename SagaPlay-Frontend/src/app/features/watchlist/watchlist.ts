export interface WatchList {
    WatchListId: number;
    WatchListItems: WatchListItem[];
}

interface WatchListItem {
    WatchListItemId:number;
    ContentItemId: number;
    WatchStatus: string;
}