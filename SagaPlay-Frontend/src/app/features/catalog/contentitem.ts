export interface ContentItem{
    Id: number;    
    Title: string;
    PlotSummary: string;
    ReleaseDate: string;
    Genre: string;
    Director: string;
    Rating: string;
    PosterURL: string;
    MainCast: CastMemberDTO[];
}

interface CastMemberDTO{    
    Id:number;
    Name:string;
    Gender:string;
}