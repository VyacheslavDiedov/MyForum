using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Channels;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyForum.Data;
using MyForum.ViewModels;

namespace MyForum.Controllers
{
    public class PostsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private static int _topicId;
        List<User> _users;
        List<Post> _posts;

        public PostsController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: Posts
        [HttpGet]
        [Route("index/{topicId:int?}")]
        public async Task<IActionResult> Index(int? topicId)
        {
            PostsViewModels pvm = new PostsViewModels { Posts = _posts, Users = _users };
            _posts = new List<Post>(_context.Posts);
            _users = new List<User>(_context.Users);
            
            if (topicId != null)
            {
                _topicId = Convert.ToInt32(topicId);
            }
            ViewBag.NameUser = User.Identity.Name;
            ViewBag.NameTopics = _context.Topics.Find(_topicId)?.Name;
            ViewBag.CountPosts = _context.Posts.Where(p => p.TopicId == _topicId).Count();
            pvm.Posts = _posts.Where(p => p.TopicId == _topicId);
            pvm.Users = _users;
            return View(pvm);
        }

       

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PostTitle,PostBody,AddPost")] Post post)
        {
            post.AddPost = DateTime.Now;
            post.UserName = User.Identity.Name;
            post.TopicId = _topicId;
            if (ModelState.IsValid)
            {
                _context.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            return View(post);
        }

        // GET: Posts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PostTitle,PostBody")] Post post)
        {
            post.AddPost = DateTime.Now;
            post.UserName = User.Identity.Name;
            post.TopicId = _topicId;
            if (id != post.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(post);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["TopicId"] = new SelectList(_context.Topics, "Id", "Id", post.TopicId);
            return View(post);
        }

        // GET: Posts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .Include(p => p.Topic)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(int id)
        {
            return _context.Posts.Any(e => e.Id == id);
        }
    }
}
