using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using SQLite;//����������� sql

namespace TrainingApp
{
    class Profile
    {
        [PrimaryKey, AutoIncrement]//�������� �������� ��
        public int Id { get; set; }//�� �������
        public string Title { get; set; }//�������� �������
        public double Height { get; set; }//����
        public double Weight { get; set; }//���
        public int Age { get; set; }//�������
        public int Sex { get; set; }//���
        public int Purpose { get; set; }//����
        public int CountTrainings { get; set; }//���-�� ����������

        //������� � ������ ���� ����������
        public override string ToString()
        {
            return String.Format("��������={0}\n����={1}\n���={2}\n�������={3}\n���={4}\n����={5}\n���������� � ������={6}",Title, Height, Weight, Age, Sex==1?"�������":"�������", Purpose == 0 ? "�����" : "����� ����", CountTrainings);
        }
    }
}