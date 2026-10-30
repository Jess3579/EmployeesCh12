using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EmployeesCh12.Models;

namespace EmployeesCh12.Controllers
{
    public class DepartmentLocationsController : Controller
    {
        private readonly EmployeeContext _context;

        public DepartmentLocationsController(EmployeeContext context)
        {
            _context = context;
        }

        // GET: DepartmentLocations
        public async Task<IActionResult> Index()
        {
            var employeeContext = _context.DepartmentLocations
                .Include(d => d.Department)
                .Include(d => d.Location);

            return View(await employeeContext.ToListAsync());
        }

        // GET: DepartmentLocations/Details
        public async Task<IActionResult> Details(int? DepartmentID, int? LocationID)
        {
            if (DepartmentID == null || LocationID == null)
            {
                return NotFound();
            }

            var departmentLocation = await _context.DepartmentLocations
                .Include(d => d.Department)
                .Include(d => d.Location)
                .FirstOrDefaultAsync(m =>
                    m.DepartmentID == DepartmentID &&
                    m.LocationID == LocationID);

            if (departmentLocation == null)
            {
                return NotFound();
            }

            return View(departmentLocation);
        }

        // GET: DepartmentLocations/Create
        public IActionResult Create()
        {
            ViewData["DepartmentID"] = new SelectList(
                _context.Departments,
                "ID",
                "Name"
            );

            ViewData["LocationID"] = new SelectList(
                _context.Locations,
                "ID",
                "Address"
            );

            return View();
        }

        // POST: DepartmentLocations/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("DepartmentID,LocationID")] DepartmentLocation departmentLocation)
        {
            bool exists = _context.DepartmentLocations.Any(d =>
                d.DepartmentID == departmentLocation.DepartmentID &&
                d.LocationID == departmentLocation.LocationID);

            if (exists)
            {
                ModelState.AddModelError(
                    "",
                    "This Department and Location combination already exists."
                );
            }

            if (ModelState.IsValid)
            {
                _context.Add(departmentLocation);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewData["DepartmentID"] = new SelectList(
                _context.Departments,
                "ID",
                "Name",
                departmentLocation.DepartmentID
            );

            ViewData["LocationID"] = new SelectList(
                _context.Locations,
                "ID",
                "Address",
                departmentLocation.LocationID
            );

            return View(departmentLocation);
        }

        // GET: DepartmentLocations/Delete
        public async Task<IActionResult> Delete(int? DepartmentID, int? LocationID)
        {
            if (DepartmentID == null || LocationID == null)
            {
                return NotFound();
            }

            var departmentLocation = await _context.DepartmentLocations
                .Include(d => d.Department)
                .Include(d => d.Location)
                .FirstOrDefaultAsync(m =>
                    m.DepartmentID == DepartmentID &&
                    m.LocationID == LocationID);

            if (departmentLocation == null)
            {
                return NotFound();
            }

            return View(departmentLocation);
        }

        // POST: DepartmentLocations/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(
            int DepartmentID,
            int LocationID)
        {
            var departmentLocation =
                await _context.DepartmentLocations.FindAsync(
                    DepartmentID,
                    LocationID
                );

            if (departmentLocation != null)
            {
                _context.DepartmentLocations.Remove(departmentLocation);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}