﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;


namespace WpfApp3
{
    public class Tri_Intersect
    {
        public static Vector3D source = new Vector3D();
        public static Vector3D target = new Vector3D();

        #region variables

        static int coplanar;
        static Vector3D p1 = new Vector3D();
        static Vector3D q1 = new Vector3D();
        static Vector3D r1 = new Vector3D();
        static Vector3D p2 = new Vector3D();
        static Vector3D q2 = new Vector3D();
        static Vector3D r2 = new Vector3D();

        static double dp1, dq1, dr1, dp2, dq2, dr2;
        static Vector3D v1 = new Vector3D();
        static Vector3D v2 = new Vector3D();
        static Vector3D v = new Vector3D();
        static Vector3D N1 = new Vector3D();
        static Vector3D N2 = new Vector3D();
        static Vector3D N = new Vector3D();

        #endregion

        public static Vector3D ConvertToVector3D(Point3D p)
        {
            return new Vector3D(p.X,p.Y,p.Z);
        }
        public static int Intersection_3d(Point3D P1, Point3D Q1, Point3D R1, Point3D P2, Point3D Q2, Point3D R2)
        {


            return Intersection_3d(
                ConvertToVector3D(P1),
                ConvertToVector3D(Q1),
                ConvertToVector3D(R1),
                ConvertToVector3D(P2),
                ConvertToVector3D(Q2),
                ConvertToVector3D(R2));
        }

        // Three-dimensional Triangle-Triangle Intersection
            // ================================================ 
            // The following version computes the segment of intersection of the two triangles if it exists. 
            //   - coplanar returns whether the triangles are coplanar
            //   - source and target are the endpoints of the line segment of intersection 
            public static int Intersection_3d(Vector3D P1, Vector3D Q1, Vector3D R1, Vector3D P2, Vector3D Q2, Vector3D R2)
        {
            /*
                The following version computes the segment of intersection of the two triangles if it exists. 
                - coplanar returns whether the triangles are coplanar
                - source and target are the endpoints of the line segment of intersection 
            */
            p1.X = P1.X;
            p1.Y = P1.Y;
            p1.Z = P1.Z;
            q1.X = Q1.X;
            q1.Y = Q1.Y;
            q1.Z = Q1.Z;
            r1.X = R1.X;
            r1.Y = R1.Y;
            r1.Z = R1.Z;
            p2.X = P2.X;
            p2.Y = P2.Y;
            p2.Z = P2.Z;
            q2.X = Q2.X;
            q2.Y = Q2.Y;
            q2.Z = Q2.Z;
            r2.X = R2.X;
            r2.Y = R2.Y;
            r2.Z = R2.Z;

            // Compute distance signs of p1, q1 and r1 to the plane of triangle(p2,q2,r2)

            //SUB(v1, p2, r2);
            v1.X = p2.X - r2.X;
            v1.Y = p2.Y - r2.Y;
            v1.Z = p2.Z - r2.Z;
            //SUB(v2, q2, r2);
            v2.X = q2.X - r2.X;
            v2.Y = q2.Y - r2.Y;
            v2.Z = q2.Z - r2.Z;
            //CROSS(N2, v1, v2);
            N2.X = v1.Y * v2.Z - v1.Z * v2.Y;
            N2.Y = v1.Z * v2.X - v1.X * v2.Z;
            N2.Z = v1.X * v2.Y - v1.Y * v2.X;

            //SUB(v1, p1, r2);
            v1.X = p1.X - r2.X;
            v1.Y = p1.Y - r2.Y;
            v1.Z = p1.Z - r2.Z;
            //dp1 = DOT(v1, N2);
            dp1 = v1.X * N2.X + v1.Y * N2.Y + v1.Z * N2.Z;

            //SUB(v1, q1, r2);
            v1.X = q1.X - r2.X;
            v1.Y = q1.Y - r2.Y;
            v1.Z = q1.Z - r2.Z;
            //dq1 = DOT(v1, N2);
            dq1 = v1.X * N2.X + v1.Y * N2.Y + v1.Z * N2.Z;
            //SUB(v1, r1, r2);
            v1.X = r1.X - r2.X;
            v1.Y = r1.Y - r2.Y;
            v1.Z = r1.Z - r2.Z;
            //dr1 = DOT(v1, N2);
            dr1 = v1.X * N2.X + v1.Y * N2.Y + v1.Z * N2.Z;

            if (((dp1 * dq1) > 0f) && ((dp1 * dr1) > 0f)) return 0;

            // Compute distance signs of p2, q2 and r2 to the plane of triangle(p1,q1,r1)

            //SUB(v1, q1, p1);
            v1.X = q1.X - p1.X;
            v1.Y = q1.Y - p1.Y;
            v1.Z = q1.Z - p1.Z;
            //SUB(v2, r1, p1);
            v2.X = r1.X - p1.X;
            v2.Y = r1.Y - p1.Y;
            v2.Z = r1.Z - p1.Z;
            //CROSS(N1, v1, v2);
            N1.X = v1.Y * v2.Z - v1.Z * v2.Y;
            N1.Y = v1.Z * v2.X - v1.X * v2.Z;
            N1.Z = v1.X * v2.Y - v1.Y * v2.X;

            //SUB(v1, p2, r1);
            v1.X = p2.X - r1.X;
            v1.Y = p2.Y - r1.Y;
            v1.Z = p2.Z - r1.Z;
            //dp2 = DOT(v1, N1);
            dp2 = v1.X * N1.X + v1.Y * N1.Y + v1.Z * N1.Z;
            //SUB(v1, q2, r1);
            v1.X = q2.X - r1.X;
            v1.Y = q2.Y - r1.Y;
            v1.Z = q2.Z - r1.Z;
            //dq2 = DOT(v1, N1);
            dq2 = v1.X * N1.X + v1.Y * N1.Y + v1.Z * N1.Z;
            //SUB(v1, r2, r1);
            v1.X = r2.X - r1.X;
            v1.Y = r2.Y - r1.Y;
            v1.Z = r2.Z - r1.Z;
            //dr2 = DOT(v1, N1);
            dr2 = v1.X * N1.X + v1.Y * N1.Y + v1.Z * N1.Z;

            if (((dp2 * dq2) > 0f) && ((dp2 * dr2) > 0f)) return 0;

            // Permutation in a canonical form of T1's vertices

            if (dp1 > 0f)
            {
                if (dq1 > 0f) return TRI_INTER_3D(r1, p1, q1, p2, r2, q2, dp2, dr2, dq2);
                else if (dr1 > 0f) return TRI_INTER_3D(q1, r1, p1, p2, r2, q2, dp2, dr2, dq2);
                else return TRI_INTER_3D(p1, q1, r1, p2, q2, r2, dp2, dq2, dr2);
            }
            else if (dp1 < 0f)
            {
                if (dq1 < 0f) return TRI_INTER_3D(r1, p1, q1, p2, q2, r2, dp2, dq2, dr2);
                else if (dr1 < 0f) return TRI_INTER_3D(q1, r1, p1, p2, q2, r2, dp2, dq2, dr2);
                else return TRI_INTER_3D(p1, q1, r1, p2, r2, q2, dp2, dr2, dq2);
            }
            else
            {
                if (dq1 < 0f)
                {
                    if (dr1 >= 0f) return TRI_INTER_3D(q1, r1, p1, p2, r2, q2, dp2, dr2, dq2);
                    else return TRI_INTER_3D(p1, q1, r1, p2, q2, r2, dp2, dq2, dr2);
                }
                else if (dq1 > 0f)
                {
                    if (dr1 > 0f) return TRI_INTER_3D(p1, q1, r1, p2, r2, q2, dp2, dr2, dq2);
                    else return TRI_INTER_3D(q1, r1, p1, p2, q2, r2, dp2, dq2, dr2);
                }
                else
                {
                    if (dr1 > 0f) return TRI_INTER_3D(r1, p1, q1, p2, q2, r2, dp2, dq2, dr2);
                    else if (dr1 < 0f) return TRI_INTER_3D(r1, p1, q1, p2, r2, q2, dp2, dr2, dq2);
                    else
                    {
                        // triangles are co-planar
                        coplanar = 1;
                        return COPLANAR_TRI_3D(p1, q1, r1, p2, q2, r2, N1, N2);
                    }
                }
            }
            return 0;
        }

        static int TRI_INTER_3D(Vector3D p1, Vector3D q1, Vector3D r1, Vector3D p2, Vector3D q2, Vector3D r2, double dp2,
            double dq2, double dr2)
        {
            if (dp2 > 0f)
            {
                if (dq2 > 0f) return CONSTRUCT_INTERSECTION(p1, r1, q1, r2, p2, q2);
                else if (dr2 > 0f) return CONSTRUCT_INTERSECTION(p1, r1, q1, q2, r2, p2);
                else return CONSTRUCT_INTERSECTION(p1, q1, r1, p2, q2, r2);
            }
            else if (dp2 < 0f)
            {
                if (dq2 < 0f) return CONSTRUCT_INTERSECTION(p1, q1, r1, r2, p2, q2);
                else if (dr2 < 0f) return CONSTRUCT_INTERSECTION(p1, q1, r1, q2, r2, p2);
                else return CONSTRUCT_INTERSECTION(p1, r1, q1, p2, q2, r2);
            }
            else
            {
                if (dq2 < 0f)
                {
                    if (dr2 >= 0f) return CONSTRUCT_INTERSECTION(p1, r1, q1, q2, r2, p2);
                    else return CONSTRUCT_INTERSECTION(p1, q1, r1, p2, q2, r2);
                }
                else if (dq2 > 0f)
                {
                    if (dr2 > 0f) return CONSTRUCT_INTERSECTION(p1, r1, q1, p2, q2, r2);
                    else return CONSTRUCT_INTERSECTION(p1, q1, r1, q2, r2, p2);
                }
                else
                {
                    if (dr2 > 0f) return CONSTRUCT_INTERSECTION(p1, q1, r1, r2, p2, q2);
                    else if (dr2 < 0f) return CONSTRUCT_INTERSECTION(p1, r1, q1, r2, p2, q2);
                    else
                    {
                        coplanar = 1;
                        return COPLANAR_TRI_3D(p1, q1, r1, p2, q2, r2, N1, N2);
                    }
                }
            }
        }

        static int CONSTRUCT_INTERSECTION(Vector3D p1, Vector3D q1, Vector3D r1, Vector3D p2, Vector3D q2, Vector3D r2)
        {
            // This function is called when the triangles surely intersect.
            // It constructs the segment of intersection of the two triangles if they are not coplanar.

            double alpha;

            //SUB(v1, q1, p1);
            v1.X = q1.X - p1.X;
            v1.Y = q1.Y - p1.Y;
            v1.Z = q1.Z - p1.Z;
            //SUB(v2, r2, p1);
            v2.X = r2.X - p1.X;
            v2.Y = r2.Y - p1.Y;
            v2.Z = r2.Z - p1.Z;
            //CROSS(N, v1, v2);
            N.X = v1.Y * v2.Z - v1.Z * v2.Y;
            N.Y = v1.Z * v2.X - v1.X * v2.Z;
            N.Z = v1.X * v2.Y - v1.Y * v2.X;
            //SUB(v, p2, p1);
            v.X = p2.X - p1.X;
            v.Y = p2.Y - p1.Y;
            v.Z = p2.Z - p1.Z;
            //if (DOT(v, N) > 0f)
            if ((v.X * N.X + v.Y * N.Y + v.Z * N.Z) > 0f)
            {
                //SUB(v1, r1, p1);
                v1.X = r1.X - p1.X;
                v1.Y = r1.Y - p1.Y;
                v1.Z = r1.Z - p1.Z;
                //CROSS(N, v1, v2);
                N.X = v1.Y * v2.Z - v1.Z * v2.Y;
                N.Y = v1.Z * v2.X - v1.X * v2.Z;
                N.Z = v1.X * v2.Y - v1.Y * v2.X;
                //if (DOT(v, N) <= 0f) 
                if ((v.X * N.X + v.Y * N.Y + v.Z * N.Z) <= 0f)
                {
                    //SUB(v2, q2, p1);
                    v2.X = q2.X - p1.X;
                    v2.Y = q2.Y - p1.Y;
                    v2.Z = q2.Z - p1.Z;
                    //CROSS(N, v1, v2);
                    N.X = v1.Y * v2.Z - v1.Z * v2.Y;
                    N.Y = v1.Z * v2.X - v1.X * v2.Z;
                    N.Z = v1.X * v2.Y - v1.Y * v2.X;
                    //if (DOT(v, N) > 0f)
                    if ((v.X * N.X + v.Y * N.Y + v.Z * N.Z) > 0f)
                    {
                        //SUB(v1, p1, p2);
                        v1.X = p1.X - p2.X;
                        v1.Y = p1.Y - p2.Y;
                        v1.Z = p1.Z - p2.Z;
                        //SUB(v2, p1, r1);
                        v2.X = p1.X - r1.X;
                        v2.Y = p1.Y - r1.Y;
                        v2.Z = p1.Z - r1.Z;
                        //alpha = DOT(v1, N2) / DOT(v2, N2);
                        alpha = (v1.X * N2.X + v1.Y * N2.Y + v1.Z * N2.Z) / (v2.X * N2.X + v2.Y * N2.Y + v2.Z * N2.Z);
                        //SCALAR(v1, alpha, v2);
                        v1.X = alpha * v2.X;
                        v1.Y = alpha * v2.Y;
                        v1.Z = alpha * v2.Z;
                        //SUB(source, p1, v1);
                        source.X = p1.X - v1.X;
                        source.Y = p1.Y - v1.Y;
                        source.Z = p1.Z - v1.Z;
                        //SUB(v1, p2, p1);
                        v1.X = p2.X - p1.X;
                        v1.Y = p2.Y - p1.Y;
                        v1.Z = p2.Z - p1.Z;
                        //SUB(v2, p2, r2);
                        v2.X = p2.X - r2.X;
                        v2.Y = p2.Y - r2.Y;
                        v2.Z = p2.Z - r2.Z;
                        //alpha = DOT(v1, N1) / DOT(v2, N1);
                        alpha = (v1.X * N1.X + v1.Y * N1.Y + v1.Z * N1.Z) / (v2.X * N1.X + v2.Y * N1.Y + v2.Z * N1.Z);
                        //SCALAR(v1, alpha, v2);
                        v1.X = alpha * v2.X;
                        v1.Y = alpha * v2.Y;
                        v1.Z = alpha * v2.Z;
                        //SUB(target, p2, v1);
                        target.X = p2.X - v1.X;
                        target.Y = p2.Y - v1.Y;
                        target.Z = p2.Z - v1.Z;
                        return 1;
                    }
                    else
                    {
                        //SUB(v1, p2, p1);
                        v1.X = p2.X - p1.X;
                        v1.Y = p2.Y - p1.Y;
                        v1.Z = p2.Z - p1.Z;
                        //SUB(v2, p2, q2);
                        v2.X = p2.X - q2.X;
                        v2.Y = p2.Y - q2.Y;
                        v2.Z = p2.Z - q2.Z;
                        //alpha = DOT(v1, N1) / DOT(v2, N1);
                        alpha = (v1.X * N1.X + v1.Y * N1.Y + v1.Z * N1.Z) / (v2.X * N1.X + v2.Y * N1.Y + v2.Z * N1.Z);
                        //SCALAR(v1, alpha, v2);
                        v1.X = alpha * v2.X;
                        v1.Y = alpha * v2.Y;
                        v1.Z = alpha * v2.Z;
                        //SUB(source, p2, v1);
                        source.X = p2.X - v1.X;
                        source.Y = p2.Y - v1.Y;
                        source.Z = p2.Z - v1.Z;
                        //SUB(v1, p2, p1);
                        v1.X = p2.X - p1.X;
                        v1.Y = p2.Y - p1.Y;
                        v1.Z = p2.Z - p1.Z;
                        //SUB(v2, p2, r2);
                        v2.X = p2.X - r2.X;
                        v2.Y = p2.Y - r2.Y;
                        v2.Z = p2.Z - r2.Z;
                        //alpha = DOT(v1, N1) / DOT(v2, N1);
                        alpha = (v1.X * N1.X + v1.Y * N1.Y + v1.Z * N1.Z) / (v2.X * N1.X + v2.Y * N1.Y + v2.Z * N1.Z);
                        //SCALAR(v1, alpha, v2);
                        v1.X = alpha * v2.X;
                        v1.Y = alpha * v2.Y;
                        v1.Z = alpha * v2.Z;
                        //SUB(target, p2, v1);
                        target.X = p2.X - v1.X;
                        target.Y = p2.Y - v1.Y;
                        target.Z = p2.Z - v1.Z;
                        return 1;
                    }
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                //SUB(v2, q2, p1);
                v2.X = q2.X - p1.X;
                v2.Y = q2.Y - p1.Y;
                v2.Z = q2.Z - p1.Z;
                //CROSS(N, v1, v2);
                N.X = v1.Y * v2.Z - v1.Z * v2.Y;
                N.Y = v1.Z * v2.X - v1.X * v2.Z;
                N.Z = v1.X * v2.Y - v1.Y * v2.X;
                //if (DOT(v, N) < 0f)
                if ((v.X * N.X + v.Y * N.Y + v.Z * N.Z) < 0f)
                {
                    return 0;
                }
                else
                {
                    //SUB(v1, r1, p1);
                    v1.X = r1.X - p1.X;
                    v1.Y = r1.Y - p1.Y;
                    v1.Z = r1.Z - p1.Z;
                    //CROSS(N, v1, v2);
                    N.X = v1.Y * v2.Z - v1.Z * v2.Y;
                    N.Y = v1.Z * v2.X - v1.X * v2.Z;
                    N.Z = v1.X * v2.Y - v1.Y * v2.X;
                    //if (DOT(v, N) >= 0f)
                    if ((v.X * N.X + v.Y * N.Y + v.Z * N.Z) > 0f)
                    {
                        //SUB(v1, p1, p2);
                        v1.X = p1.X - p2.X;
                        v1.Y = p1.Y - p2.Y;
                        v1.Z = p1.Z - p2.Z;
                        //SUB(v2, p1, r1);
                        v2.X = p1.X - r1.X;
                        v2.Y = p1.Y - r1.Y;
                        v2.Z = p1.Z - r1.Z;
                        //alpha = DOT(v1, N2) / DOT(v2, N2);
                        alpha = (v1.X * N2.X + v1.Y * N2.Y + v1.Z * N2.Z) / (v2.X * N2.X + v2.Y * N2.Y + v2.Z * N2.Z);
                        //SCALAR(v1, alpha, v2);
                        v1.X = alpha * v2.X;
                        v1.Y = alpha * v2.Y;
                        v1.Z = alpha * v2.Z;
                        //SUB(source, p1, v1);
                        source.X = p1.X - v1.X;
                        source.Y = p1.Y - v1.Y;
                        source.Z = p1.Z - v1.Z;
                        //SUB(v1, p1, p2);
                        v1.X = p1.X - p2.X;
                        v1.Y = p1.Y - p2.Y;
                        v1.Z = p1.Z - p2.Z;
                        //SUB(v2, p1, q1);
                        v2.X = p1.X - q1.X;
                        v2.Y = p1.Y - q1.Y;
                        v2.Z = p1.Z - q1.Z;
                        //alpha = DOT(v1, N2) / DOT(v2, N2);
                        alpha = (v1.X * N2.X + v1.Y * N2.Y + v1.Z * N2.Z) / (v2.X * N2.X + v2.Y * N2.Y + v2.Z * N2.Z);
                        //SCALAR(v1, alpha, v2);
                        v1.X = alpha * v2.X;
                        v1.Y = alpha * v2.Y;
                        v1.Z = alpha * v2.Z;
                        //SUB(target, p1, v1);
                        target.X = p1.X - v1.X;
                        target.Y = p1.Y - v1.Y;
                        target.Z = p1.Z - v1.Z;
                        return 1;
                    }
                    else
                    {
                        //SUB(v1, p2, p1);
                        v1.X = p2.X - p1.X;
                        v1.Y = p2.Y - p1.Y;
                        v1.Z = p2.Z - p1.Z;
                        //SUB(v2, p2, q2);
                        v2.X = p2.X - q2.X;
                        v2.Y = p2.Y - q2.Y;
                        v2.Z = p2.Z - q2.Z;
                        //alpha = DOT(v1, N1) / DOT(v2, N1);
                        alpha = (v1.X * N1.X + v1.Y * N1.Y + v1.Z * N1.Z) / (v2.X * N1.X + v2.Y * N1.Y + v2.Z * N1.Z);
                        //SCALAR(v1, alpha, v2);
                        v1.X = alpha * v2.X;
                        v1.Y = alpha * v2.Y;
                        v1.Z = alpha * v2.Z;
                        //SUB(source, p2, v1);
                        source.X = p2.X - v1.X;
                        source.Y = p2.Y - v1.Y;
                        source.Z = p2.Z - v1.Z;
                        //SUB(v1, p1, p2);
                        v1.X = p1.X - p2.X;
                        v1.Y = p1.Y - p2.Y;
                        v1.Z = p1.Z - p2.Z;
                        //SUB(v2, p1, q1);
                        v2.X = p1.X - q1.X;
                        v2.Y = p1.Y - q1.Y;
                        v2.Z = p1.Z - q1.Z;
                        //alpha = DOT(v1, N2) / DOT(v2, N2);
                        alpha = (v1.X * N2.X + v1.Y * N2.Y + v1.Z * N2.Z) / (v2.X * N2.X + v2.Y * N2.Y + v2.Z * N2.Z);
                        //SCALAR(v1, alpha, v2);
                        v1.X = alpha * v2.X;
                        v1.Y = alpha * v2.Y;
                        v1.Z = alpha * v2.Z;
                        //SUB(target, p1, v1);
                        target.X = p1.X - v1.X;
                        target.Y = p1.Y - v1.Y;
                        target.Z = p1.Z - v1.Z;
                        return 1;
                    }
                }
            }
        }

        static int COPLANAR_TRI_3D(Vector3D p1, Vector3D q1, Vector3D r1, Vector3D p2, Vector3D q2, Vector3D r2,
            Vector3D normal_1, Vector3D normal_2)
        {

            Vector3D P1 = new Vector3D();
            Vector3D Q1 = new Vector3D();
            Vector3D R1 = new Vector3D();
            Vector3D P2 = new Vector3D();
            Vector3D Q2 = new Vector3D();
            Vector3D R2 = new Vector3D();
            //double P2.Z,Q2.Z,R2.Z;

            double n_x, n_y, n_z;

            n_x = ((normal_1.X < 0) ? -normal_1.X : normal_1.X);
            n_y = ((normal_1.Y < 0) ? -normal_1.Y : normal_1.Y);
            n_z = ((normal_1.Z < 0) ? -normal_1.Z : normal_1.Z);


            // Projection of the triangles in 3D onto 2D such that the area of the projection is maximized
            if ((n_x > n_z) && (n_x >= n_y))
            {
                // Project onto plane YZ

                P1.X = q1.Z;
                P1.Y = q1.Y;
                Q1.X = p1.Z;
                Q1.Y = p1.Y;
                R1.X = r1.Z;
                R1.Y = r1.Y;

                P2.X = q2.Z;
                P2.Y = q2.Y;
                Q2.X = p2.Z;
                Q2.Y = p2.Y;
                R2.X = r2.Z;
                R2.Y = r2.Y;
            }
            else if ((n_y > n_z) && (n_y >= n_x))
            {
                // Project onto plane XZ

                P1.X = q1.X;
                P1.Y = q1.Z;
                Q1.X = p1.X;
                Q1.Y = p1.Z;
                R1.X = r1.X;
                R1.Y = r1.Z;

                P2.X = q2.X;
                P2.Y = q2.Z;
                Q2.X = p2.X;
                Q2.Y = p2.Z;
                R2.X = r2.X;
                R2.Y = r2.Z;
            }
            else
            {
                // Project onto plane XY

                P1.X = p1.X;
                P1.Y = p1.Y;
                Q1.X = q1.X;
                Q1.Y = q1.Y;
                R1.X = r1.X;
                R1.Y = r1.Y;

                P2.X = p2.X;
                P2.Y = p2.Y;
                Q2.X = q2.X;
                Q2.Y = q2.Y;
                R2.X = r2.X;
                R2.Y = r2.Y;
            }
            return TRI_OVERLAP_2D(P1, Q1, R1, P2, Q2, R2);
        }

        // Two dimensional Triangle-Triangle Overlap Test
        // ==============================================
        static int TRI_OVERLAP_2D(Vector3D p1, Vector3D q1, Vector3D r1, Vector3D p2, Vector3D q2, Vector3D r2)
        {
            if (ORIENT_2D(p1, q1, r1) < 0f)
                if (ORIENT_2D(p2, q2, r2) < 0f) return CCW_TRI_INTERSECTION_2D(p1, r1, q1, p2, r2, q2);
                else return CCW_TRI_INTERSECTION_2D(p1, r1, q1, p2, q2, r2);
            else if (ORIENT_2D(p2, q2, r2) < 0f) return CCW_TRI_INTERSECTION_2D(p1, q1, r1, p2, r2, q2);
            else return CCW_TRI_INTERSECTION_2D(p1, q1, r1, p2, q2, r2);
        }

        static double ORIENT_2D(Vector3D a, Vector3D b, Vector3D c)
        {
            return ((a.X - c.X) * (b.Y - c.Y) - (a.Y - c.Y) * (b.X - c.X));
        }

        static int CCW_TRI_INTERSECTION_2D(Vector3D p1, Vector3D q1, Vector3D r1, Vector3D p2, Vector3D q2, Vector3D r2)
        {
            if (ORIENT_2D(p2, q2, p1) >= 0f)
            {
                if (ORIENT_2D(q2, r2, p1) >= 0f)
                {
                    if (ORIENT_2D(r2, p2, p1) >= 0f) return 1;
                    else return INTERSECTION_TEST_EDGE(p1, q1, r1, p2, q2, r2);
                }
                else
                {
                    if (ORIENT_2D(r2, p2, p1) >= 0f) return INTERSECTION_TEST_EDGE(p1, q1, r1, r2, p2, q2);
                    else return INTERSECTION_TEST_VERTEX(p1, q1, r1, p2, q2, r2);
                }
            }
            else
            {
                if (ORIENT_2D(q2, r2, p1) >= 0f)
                {
                    if (ORIENT_2D(r2, p2, p1) >= 0f) return INTERSECTION_TEST_EDGE(p1, q1, r1, q2, r2, p2);
                    else return INTERSECTION_TEST_VERTEX(p1, q1, r1, q2, r2, p2);
                }
                else return INTERSECTION_TEST_VERTEX(p1, q1, r1, r2, p2, q2);
            }
        }

        static int INTERSECTION_TEST_EDGE(Vector3D P1, Vector3D Q1, Vector3D R1, Vector3D P2, Vector3D Q2, Vector3D R2)
        {
            if (ORIENT_2D(R2, P2, Q1) >= 0f)
            {
                if (ORIENT_2D(P1, P2, Q1) >= 0f)
                {
                    if (ORIENT_2D(P1, Q1, R2) >= 0f) return 1;
                    else return 0;
                }
                else
                {
                    if (ORIENT_2D(Q1, R1, P2) >= 0f)
                    {
                        if (ORIENT_2D(R1, P1, P2) >= 0f) return 1;
                        else return 0;
                    }
                    else return 0;
                }
            }
            else
            {
                if (ORIENT_2D(R2, P2, R1) >= 0f)
                {
                    if (ORIENT_2D(P1, P2, R1) >= 0f)
                    {
                        if (ORIENT_2D(P1, R1, R2) >= 0f) return 1;
                        else
                        {
                            if (ORIENT_2D(Q1, R1, R2) >= 0f) return 1;
                            else return 0;
                        }
                    }
                    else return 0;
                }
                else return 0;
            }
        }

        static int INTERSECTION_TEST_VERTEX(Vector3D P1, Vector3D Q1, Vector3D R1, Vector3D P2, Vector3D Q2, Vector3D R2)
        {
            if (ORIENT_2D(R2, P2, Q1) >= 0f)
            {
                if (ORIENT_2D(R2, Q2, Q1) <= 0f)
                {
                    if (ORIENT_2D(P1, P2, Q1) > 0f)
                    {
                        if (ORIENT_2D(P1, Q2, Q1) <= 0f) return 1;
                        else return 0;
                    }
                    else
                    {
                        if (ORIENT_2D(P1, P2, R1) >= 0f)
                            if (ORIENT_2D(Q1, R1, P2) >= 0f) return 1;
                            else return 0;
                        else return 0;
                    }
                }
                else
                {
                    if (ORIENT_2D(P1, Q2, Q1) <= 0f)
                    {
                        if (ORIENT_2D(R2, Q2, R1) <= 0f)
                        {
                            if (ORIENT_2D(Q1, R1, Q2) >= 0f) return 1;
                            else return 0;
                        }
                        else return 0;
                    }
                    else return 0;
                }
            }
            else
            {
                if (ORIENT_2D(R2, P2, R1) >= 0f)
                {
                    if (ORIENT_2D(Q1, R1, R2) >= 0f)
                    {
                        if (ORIENT_2D(P1, P2, R1) >= 0f) return 1;
                        else return 0;
                    }
                    else
                    {
                        if (ORIENT_2D(Q1, R1, Q2) >= 0f)
                        {
                            if (ORIENT_2D(R2, R1, Q2) >= 0f) return 1;
                            else return 0;
                        }
                        else return 0;
                    }
                }
                else return 0;
            }
        }

    }
}
