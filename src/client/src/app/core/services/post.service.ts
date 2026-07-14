import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, forkJoin, map, of, switchMap } from 'rxjs';

import { Comment } from '../models/comment.model';
import { Post } from '../models/post.model';
import { environment } from '../../../environments/environment';

interface ApiResponse<T> {
  succeeded: boolean;
  message?: string;
  data: T;
}

interface ApiComment {
  id: number;
  postId: number;
  commentBy: string;
  message: string;
  createdAt: string;
}

interface ApiPost {
  id: number;
  title: string;
  imageUrl: string | null;
  createdBy: string | null;
  createdAt: string;
}

@Injectable({
  providedIn: 'root'
})
export class PostService {
  private readonly apiBaseUrl = environment.apiBaseUrl;

  constructor(private readonly http: HttpClient) {}

  getPosts(): Observable<Post[]> {
    return this.http
      .get<ApiResponse<ApiPost[]>>(`${this.apiBaseUrl}/api/post`)
      .pipe(
        switchMap((response) => {
          const posts = response.data ?? [];

          if (posts.length === 0) {
            return of([]);
          }

          return forkJoin(
            posts.map((post) =>
              this.getComments(post.id).pipe(map((comments) => this.mapPost(post, comments)))
            )
          );
        })
      );
  }

  addComment(postId: number, message: string): Observable<Comment[]> {
    return this.http
      .post<ApiResponse<ApiComment[]>>(`${this.apiBaseUrl}/api/comment`, {
        postId,
        commentBy: 'Blend 285',
        message
      })
      .pipe(switchMap(() => this.getComments(postId)));
  }

  getComments(postId: number): Observable<Comment[]> {
    return this.http
      .get<ApiResponse<ApiComment[]>>(`${this.apiBaseUrl}/api/comment`, {
        params: { postId }
      })
      .pipe(map((response) => (response.data ?? []).map((comment) => this.mapComment(comment))));
  }

  private mapPost(post: ApiPost, comments: Comment[] = []): Post {
    return {
      id: post.id,
      title: post.title,
      authorName: post.createdBy ?? '',
      authorAvatarUrl: '',
      createdAt: post.createdAt,
      imageUrl: post.imageUrl ?? '',
      comments
    };
  }

  private mapComment(comment: ApiComment): Comment {
    return {
      id: comment.id,
      authorName: comment.commentBy,
      authorAvatarUrl: '',
      message: comment.message,
      createdAt: comment.createdAt
    };
  }
}
