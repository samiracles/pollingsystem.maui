using System;
using System.Collections.Generic;
using PollingSystem.MAUI.Models;

namespace PollingSystem.MAUI.Services
{
    public class CacheService
    {
        private static CacheService _instance;
        public static CacheService Instance => _instance ??= new CacheService();

        private readonly Dictionary<string, object> _cache;
        private const string PollListCacheKey = "PollList";
        private const string VoteListCacheKey = "VoteList";
        private const string CurrentUserCacheKey = "CurrentUser";
        

        private CacheService()
        {
            _cache = new Dictionary<string, object>();
        }

        public void Set<T>(string key, T value)
        {
            _cache[key] = value;
        }

        public T Get<T>(string key)
        {
            if (_cache.TryGetValue(key, out var value))
            {
                return (T)value;
            }
            return default;
        }

        public bool ContainsKey(string key)
        {
            return _cache.ContainsKey(key);
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }

        public void Clear()
        {
            _cache.Clear();
        }

        // Methods to manage the list of polls
        public void AddPoll(Poll poll)
        {
            var polls = GetPollList();
            polls.Add(poll);
            Set(PollListCacheKey, polls);
        }

        public List<Poll> GetPollList()
        {
            if (ContainsKey(PollListCacheKey))
            {
                return Get<List<Poll>>(PollListCacheKey);
            }
            return new List<Poll>();
        }

        public void RemovePoll(Poll poll)
        {
            var polls = GetPollList();
            polls.Remove(poll);
            Set(PollListCacheKey, polls);
        }

        // Methods to manage the list of votes
        public void AddVote(Vote vote)
        {
            var votes = GetVoteList();
            votes.Add(vote);
            Set(VoteListCacheKey, votes);
        }

        public List<Vote> GetVoteList()
        {
            if (ContainsKey(VoteListCacheKey))
            {
                return Get<List<Vote>>(VoteListCacheKey);
            }
            return new List<Vote>();
        }

        public void RemoveVote(Vote vote)
        {
            var votes = GetVoteList();
            votes.Remove(vote);
            Set(VoteListCacheKey, votes);
        }

        // Methods to manage the current user
        public void SetCurrentUser(User user)
        {
            Set(CurrentUserCacheKey, user);
        }

        public User GetCurrentUser()
        {
            return Get<User>(CurrentUserCacheKey);
        }

        public void RemoveCurrentUser()
        {
            Remove(CurrentUserCacheKey);
        }
    }
}
