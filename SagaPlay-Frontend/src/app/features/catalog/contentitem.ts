export interface ContentItem{
    id: number;
    title: string;
    plotSummary: string;
    releaseDate: string;
    genre: string;
    director: string;
    rating: string;
    posterURL: string;
    mainCast: CastMemberDTO[];
}

interface CastMemberDTO{    
    Id:number;
    Name:string;
    Gender:string;
}