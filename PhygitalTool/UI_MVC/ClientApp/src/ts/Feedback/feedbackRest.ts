import {types} from "sass";
import Number = types.Number;

export async function readReactions(postId: number): Promise<ReactionRead[]> {
    const response = await fetch(`/api/feedbacks/${postId}/Reactions`);
    if (!response.ok) {
        throw new Error("Error fetching reactions");
    }
    return await response.json();
}

export async function createReaction(postId: number, reaction: Reaction): Promise<Reaction> {
    const response = await fetch(`/api/feedbacks/${postId}/AddReaction`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(reaction)
    });
    if (!response.ok) {
        console.error(JSON.stringify(reaction));
        throw new Error("Error creating reaction");
    }
    return await response.json();
}

export async function createLikePost(postId: number): Promise<{ likeCount: number, dislikeCount: number }> {
    const response = await fetch(`/api/feedbacks/${postId}/LikePost`, {
        method: 'POST'
    });
    if (!response.ok) {
        throw new Error("Error liking post");
    }
    return await response.json();
}

export async function createDislikePost(postId: number): Promise<{ likeCount: number, dislikeCount: number }> {
    const response = await fetch(`/api/feedbacks/${postId}/DislikePost`, {
        method: 'POST'
    });
    if (!response.ok) {
        throw new Error("Error disliking post");
    }
    return await response.json();
}