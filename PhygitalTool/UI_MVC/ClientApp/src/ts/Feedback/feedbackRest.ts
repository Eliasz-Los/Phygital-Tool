

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
        const error = await response.json();
        throw {status: response.status, message: error};
    }
    return await response.json();
}

export async function deleteReaction(postId: number, reactionId: number): Promise<void> {
    const response = await fetch(`/api/feedbacks/${postId}/DeleteReaction/${reactionId}`, {
        method: 'POST'
    });
    if (!response.ok) {
       const error = await response.text();
       throw {status: response.status, message: error.toString()};
    }
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