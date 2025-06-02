$(document).ready(function() {
    loadStudents();

    $('form').on('submit', function(e) {
        e.preventDefault();
        
        const firstName = $('#firstName').val();
        const lastName = $('#lastName').val();
        const age = $('#age').val();
        const photoFile = $('#photo')[0].files[0];
        
        if (!firstName || !lastName || !age) {
            alert('Please fill in all required fields');
            return;
        }
        
        let photoUrl = 'default-avatar.png';
        if (photoFile) {
            photoUrl = URL.createObjectURL(photoFile);
        }
        
        const student = {
            firstName: firstName,
            lastName: lastName,
            age: age,
            photoUrl: photoUrl
        };
        
        saveStudent(student);
        addStudentToTable(student);
        this.reset();
    });
    
    $(document).on('click', '.btn-danger', function() {
        if (confirm('Are you sure you want to delete this student?')) {
            const row = $(this).closest('tr');
            const firstName = row.find('td:eq(1)').text();
            const lastName = row.find('td:eq(2)').text();
            
            deleteStudent(firstName, lastName);
            row.remove();
        }
    });
    
    $(document).on('click', '.btn-primary', function() {
        const row = $(this).closest('tr');
        const firstName = row.find('td:eq(1)').text();
        const lastName = row.find('td:eq(2)').text();
        const age = row.find('td:eq(3)').text();
        
        $('#firstName').val(firstName);
        $('#lastName').val(lastName);
        $('#age').val(age);
        
        row.remove();
    });
});

function saveStudent(student) {
    let students = JSON.parse(localStorage.getItem('students') || '[]');
    students.push(student);
    localStorage.setItem('students', JSON.stringify(students));
}

function loadStudents() {
    let students = JSON.parse(localStorage.getItem('students') || '[]');
    students.forEach(student => {
        addStudentToTable(student);
    });
}

function deleteStudent(firstName, lastName) {
    let students = JSON.parse(localStorage.getItem('students') || '[]');
    students = students.filter(student => 
        !(student.firstName === firstName && student.lastName === lastName)
    );
    localStorage.setItem('students', JSON.stringify(students));
}

function addStudentToTable(student) {
    const newRow = `
        <tr>
            <td><img src="${student.photoUrl}" alt="Student Photo" class="img-responsive img-circle" style="width: 50px; height: 50px;"></td>
            <td>${student.firstName}</td>
            <td>${student.lastName}</td>
            <td>${student.age}</td>
            <td>
                <button class="btn btn-primary btn-xs action-btn">E</button>
                <button class="btn btn-danger btn-xs action-btn">D</button>
            </td>
        </tr>
    `;
    $('tbody').append(newRow);
} 